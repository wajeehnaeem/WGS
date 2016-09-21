﻿(function (factory) {

    if (typeof define === "function" && define.amd) {
        define('bforms-extensions', ['jquery'], factory);
    } else {
        factory(window.jQuery);
    }

})(function ($, undefined) {

    //#region bsFillForm
    $.fn.bsFillForm = function (model, prefix) {
        var $elem = $(this),
            formPrefix = typeof prefix !== "undefined" ? prefix : '';

        if ($elem.length && model != null) {
            var $inputs = $elem.find('input,select,textarea');

            $inputs.each(function (idx, input) {
                var $input = $(input),
                    inputName = $input.attr('name'),
                    propertyName = inputName.replace(formPrefix, ''),
                    possibleValue = model[propertyName];

                if (typeof possibleValue !== "undefined") {
                    $input.val(possibleValue);
                }
            });

        }

        return $elem;
    };
    //#endregion

    //#region parse form
    $.fn.parseForm = function (prefix) {

        // don't change the element sent, we allow jq objects or selectors too
        var $elem = $(this);

        // object to be returned and where all the data goes
        var data = {};

        if ($elem.length > 0) {

            // NORMAL INPUT FIELDS

            // input and select fields 
            var input = $elem.find('input[type!="radio"], input[type="radio"]:checked, select, textarea, .checkBoxList-done');

            for (var key in input) {
                if (!isNaN(key)) {

                    var jqEl = $(input[key]);

                    if (jqEl.data('noparse') === true || (jqEl.prev().data('noparse') && jqEl.prev().prop('name') == jqEl.prop('name') === true))
                        continue;

                    //custom value provider?
                    if (jqEl.hasClass('checkBoxList-done')) {
                        $.extend(true, data, jqEl.bsParseCheckList());
                    } else {

                        var name = jqEl.data('formname') || jqEl.attr('name');
                        if (prefix && name) {
                            name = name.replace(prefix, "");
                        }
                        var value = jqEl.data('select2') != null ? jqEl.select2('val') : jqEl.val();

                        if ('undefined' !== typeof (name)) {

                            if (jqEl.attr('type') === 'checkbox') {
                                // checkbox
                                value = jqEl.is(':checked');
                                data[name] = value;

                            } else if ('object' !== typeof (value)) {

                                // normal input
                                if ('undefined' === typeof (data[name]))
                                    data[name] = value;

                            } else if (value !== null) {

                                // multiselect
                                for (k in value) {
                                    data[name + '[' + k + ']'] = value[k];
                                }
                            }
                        }
                    }
                }
            }

            // files
            $elem.find('input[type="file"]').each(function (k, el) {

                var files = el.files;
                if (files != undefined) {
                    var name = el.name;
                    if (prefix && name) {
                        name = name.replace(prefix, "");
                    }
                    if (files.length > 1) {
                        for (var i = 0; i < files.length; i++) {
                            data[name + '[' + i + ']'] = files[i];
                        }
                    } else if (files.length == 1) {
                        data[name] = files[0];
                    }
                }
            });

        }

        return data;
    };
    //#endregion

    //#region $.fn.bsResetForm
    $.fn.bsResetForm = function (focus, ignore, triggerChange) {

        $(this).find('input:not(.hasDatepicker, .hasRangepicker,.hasNumberRangepicker, ' + ignore + '), textarea:not(' + ignore + ')').each(function () {
            switch (this.type) {
                case 'password':
                case 'select-multiple':
                case 'select-one':
                case 'text':
                case 'url':
                case 'email':
                case 'textarea':
                    if ($(this).hasClass("tag_counter")) {
                        $(this).val('0');
                    } else {
                        $(this).val('');
                        if (triggerChange === true) {
                            $(this).trigger('change');
                        }
                    }
                    break;
                case 'checkbox':
                    if ($(this).hasClass("checkBox-done")) {
                        $(this).attr("checked", $(this).data("initialvalue"));
                        $(this).trigger("change");
                    } else {
                        $(this).attr('checked', false);
                    }
                    break;
                case 'radio':
                    this.checked = false;
                    break;
                case 'range':
                    $(this).val(0);
                    break;
                case 'file':
                    $(this).val('');
                    $(this).trigger('focusout').trigger('change');
                    break;
                case 'hidden':
                    if (typeof $(this).data('select2') !== 'undefined') {
                        $(this).select2('val', '');
                    }
            }
        });
        //#endregion

        //#region select2
        $(this).find('select' + ':not(' + ignore + ')').each(function () {
            var thisObj = $(this);
            if (typeof thisObj.data('initialvalue') !== 'undefined') {
                if (thisObj.data('select2') != null) {
                    thisObj.select2('val', thisObj.data('initialvalue'));
                } else {
                    thisObj.val(thisObj.data('initialvalue'));
                }
            }
            else {
                if (thisObj.data('select2') != null) {
                    thisObj.select2('val', '');
                } else {
                    thisObj.val('');
                }
            }

            thisObj.trigger('change');
        });
        //#endregion

        //#region radio buttons
        $(this).find(".bs-radio-list" + ':not(' + ignore + ')').each(function () {
            if ($(this).data("initialvalue") != undefined) {
                $(this).bsRadioButtonsListUpdateSelf($(this).data("initialvalue"));
            }
        });
        //#endregion

        //#region datePicker
        $(this).find('.hasDatepicker' + ':not(' + ignore + ')').each(function () {
            $(this).bsDatepicker('resetValue');
        });
        //#endregion

        //#region rangePicker
        $(this).find('.hasRangepicker' + ':not(' + ignore + ')').each(function () {
            $(this).bsDateRange('resetValue');
        });
        //#endregion

        //#region number rangePicker
        $(this).find('.hasNumberRangepicker' + ':not(' + ignore + ')').each(function () {
            $(this).bsRangePicker('resetValue');
        });
        //#endregion

        //#region radioButtonsList
        var radioButtons = $(this).find('.radioButtonsList-done' + ':not(' + ignore + ')');

        if (radioButtons.length > 0) {
            radioButtons.bsResetRadioButtons();
        }
        //#endregion

        if (focus !== false)
            $(this).find("input:first").focus();

        var $form = $([]);

        if (($(this)).is('form')) {
            $form = $(this);
        } else {
            $form = $(this).find('form');
        }

        if ($form.length) {
            $form.each(function () {
                var validator = $(this).data('validator');
                if (typeof validator !== "undefined" && typeof validator.resetForm === "function") {
                    validator.resetForm();
                }
            });
        }

        return $(this);
    };
    //#endregion

    //#region $.fn.bsResetFormInOrder
    // place data-resetorder to set the order in which the fields reset is done (in case you have some triggers that get messed up)
    $.fn.bsResetFormInOrder = function (focus, ignore, triggerChange) {

        $(this).find('input:not(' + ignore + '), textarea:not(' + ignore + '), select' + ':not(' + ignore + ') , .bs-radio-list:not(' + ignore + '), .radioButtonsList-done:not(' + ignore + ')')
            .sort(function (el1, el2) {
                var order1 = $(el1).data('resetorder'),
                    order2 = $(el2).data('resetorder');

                if (order1 && order2) {
                    return order1 - order2;
                } else if (order1) {
                    return 1;
                } else if (order2) {
                    return -1
                } else {
                    return 0;
                }
            }).each(function () {
                var thisObj = $(this);

                if (thisObj.hasClass('bs-radio-list')) {

                    thisObj.bsResetRadioButtons();

                } else if (thisObj.is('select')) {

                    if (thisObj.data('initialvalue')) {
                        if (thisObj.data('select2') != null) {
                            thisObj.select2('val', thisObj.data('initialvalue'));
                        } else {
                            thisObj.val(thisObj.data('initialvalue'));
                        }
                    }
                    else {
                        if (thisObj.data('select2') != null) {
                            thisObj.select2('val', '');
                        } else {
                            thisObj.val('');
                        }
                    }

                    thisObj.trigger('change');

                } else if (thisObj.hasClass('hasDatepicker')) {

                    thisObj.bsDatepicker('resetValue');

                } else if (thisObj.hasClass('hasRangepicker')) {

                    thisObj.bsDateRange('resetValue');

                } else if (thisObj.hasClass('hasNumberRangepicker')) {

                    thisObj.bsRangePicker('resetValue');

                } else if (thisObj.hasClass('radioButtonsList-done')) {

                    thisObj.bsResetRadioButtons();

                } else {
                    switch (this.type) {
                        case 'password':
                        case 'select-multiple':
                        case 'select-one':
                        case 'text':
                        case 'url':
                        case 'email':
                        case 'textarea':
                            if (thisObj.hasClass("tag_counter")) {
                                thisObj.val('0');
                            } else {
                                thisObj.val('');
                                if (triggerChange === true) {
                                    thisObj.trigger('change');
                                }
                            }
                            break;
                        case 'checkbox':
                            if (thisObj.hasClass("checkBox-done")) {
                                thisObj.attr("checked", thisObj.data("initialvalue"));
                                thisObj.trigger("change");
                            } else {
                                thisObj.attr('checked', false);
                            }
                            break;
                        case 'radio':
                            this.checked = false;
                            break;
                        case 'range':
                            thisObj.val(0);
                            break;
                        case 'file':
                            thisObj.val('');
                            thisObj.trigger('focusout').trigger('change');
                            break;
                        case 'hidden':
                            if (typeof thisObj.data('select2') !== 'undefined') {
                                thisObj.select2('val', '');
                            }
                    }
                }

            });

        //#endregion

        if (focus !== false)
            $(this).find("input:first").focus();

        var $form = $([]);

        if (($(this)).is('form')) {
            $form = $(this);
        } else {
            $form = $(this).find('form');
        }

        if ($form.length) {
            $form.each(function () {
                var validator = $(this).data('validator');
                if (typeof validator !== "undefined" && typeof validator.resetForm === "function") {
                    validator.resetForm();
                }
            });
        }

        return $(this);
    };
    //#endregion

    //#region $.fn.bsDisableForm
    $.fn.bsDisableForm = function (ignore) {
        $(this).find('input:not(' + ignore + '), textarea:not(' + ignore + '), select' + ':not(' + ignore + ')').each(function () {
            $(this).css({
                'cursor': 'not-allowed !important',
                'background-color': '#eeeeee !important',
                'opacity': '1 !important'
            });
            $(this).attr('disabled', 'disabled');
        });
    };
    //#endregion

    //#region $.fn.bsEnableForm
    $.fn.bsEnableForm = function (ignore) {
        $(this).find('input:not(' + ignore + '), textarea:not(' + ignore + '), select' + ':not(' + ignore + ')').each(function () {
            $(this).removeAttr('disabled');
            $(this).css({
                'cursor': 'auto',
                'background-color': 'transparent'
            });
        });
    };
    //#endregion

});
