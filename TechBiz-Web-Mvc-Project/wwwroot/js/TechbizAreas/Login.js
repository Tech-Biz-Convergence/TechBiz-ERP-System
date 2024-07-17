/**
 * Account Settings - Account
 */

'use strict';
(function () {
  console.log("URL :"+config.apiUrl.base);
  $(document).ready(function () {
    localStorage.clear();
    $('#formAuthentication').submit(function (event) {
      event.preventDefault(); // ป้องกันการรีเฟรชหน้า

      var formData = $(this).serializeObject();
      console.log(formData);
      $.ajax({
        url: config.apiUrl.base + '/api/identity/login',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(formData),
        success: function (response) {
          console.log(response);
          if (response.status == true) {
            localStorage.setItem('token', response.data.token);
            localStorage.setItem('user_name', response.data.user_name);
            console.log("Token saved to localStorage: " + response.data.token);
            MessageBox.SuccessMessage(response.description, response.code).then(() => {
              //   window.location.href = '/html/hr/employee-management/index.html';
              window.location.href = `/Dashboards/Index?user_name=${encodeURIComponent(response.data.user_name)}&token=${encodeURIComponent(response.data.token)}`;
            });
          } else {
            MessageBox.ErrorMessage(response.description, response.code).then(() => {
              $('#formAuthentication')[0].reset();
              $('#user_name').focus();
            });
          }



        },
        error: function (xhr, status, error) {
          alert("error" + error);
          console.error(xhr.responseText);
        }
      });
    });
  }); //edn ready
})();
 


