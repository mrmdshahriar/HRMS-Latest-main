(function($) {
  'use strict';
  $.validator.setDefaults({
    submitHandler: function() {
      alert("submitted!");
    }
  });
    $(function () {

    // validate the comment form when it is submitted
    $("#commentForm").validate({
      errorPlacement: function(label, element) {
        label.addClass('mt-2 text-danger');
        label.insertAfter(element);
      },
      highlight: function(element, errorClass) {
        $(element).parent().addClass('has-danger')
        $(element).addClass('form-control-danger')
      }
    });

  
  });
})(jQuery);
