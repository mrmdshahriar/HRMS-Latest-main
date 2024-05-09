(function ($) {
  'use strict';
  if ($('#timepicker-example').length) {
    $('#timepicker-example').datetimepicker({
      format: 'LT',
    });
  }
  if ($('.color-picker').length) {
    $('.color-picker').asColorPicker();
  }
  if ($('#datepickerJoining').length) {
    $('#datepickerJoining').datepicker({
      enableOnReadonly: true,
      todayHighlight: true,
      autoclose: true,
    });
  }
  if ($('#ApplyDate').length) {
    $('#ApplyDate').datepicker({
      enableOnReadonly: true,
      todayHighlight: true,
      autoclose: true,
    });
  }
  if ($('#AvailableDate').length) {
    $('#AvailableDate').datepicker({
      enableOnReadonly: true,
      todayHighlight: true,
      autoclose: true,
    });
  }

  if ($('#JoiningDate').length) {
    $('#JoiningDate').datepicker({
      enableOnReadonly: true,
      todayHighlight: true,
      autoclose: true,
    });
  }

  if ($('#DateFrom').length) {
    $('#DateFrom').datepicker({
      enableOnReadonly: true,
      todayHighlight: true,
      autoclose: true,
    });
  }

  if ($('#DateTo').length) {
    $('#DateTo').datepicker({
      enableOnReadonly: true,
      todayHighlight: true,
      autoclose: true,
    });
  }

  if ($('#inline-datepicker').length) {
    $('#inline-datepicker').datepicker({
      enableOnReadonly: true,
      todayHighlight: true,
    });
  }
  if ($('.datepicker-autoclose').length) {
    $('.datepicker-autoclose').datepicker({
      autoclose: true,
    });
  }
  if ($('.input-daterange').length) {
    $('.input-daterange input').each(function () {
      $(this).datepicker('clearDates');
    });
    $('.input-daterange').datepicker({});
  }
})(jQuery);
