/**
 * Account Settings - Account
 */

'use strict';
$(document).ready(function () {
  const token = localStorage.getItem("token");
  const username = localStorage.getItem("user_name");
  const permId = localStorage.getItem('perm_id');
 // if (!token || !username || !permId) {
  if (!token || !username ) {
    window.location.href = window.location.origin + '/Auth/LoginBasic';

  }
  console.log("token : " + username);
  console.log("username : " + username);
  console.log("perm_id : " + permId);

 // LoadPermissionFeature(permId);
  function LoadPermissionFeature(permId) {

    //$('.button-feature').hide();
    $.ajax({
      url: config.apiUrl.base + '/api/auth/permissionrolemapping/get/' + permId,
      type: 'GET',
      contentType: 'application/json; charset=utf-8',
      data: {
        user_name: username
      },
      headers: { "Authorization": "Bearer " + token },
      success: function (response) {
        console.log(response);
        if (response.status == true) {
          var res = response.data;


          $('#title-program-name').text(res.menu_name);
          $('#title-program-id').text(res.program_code);

          if (res.permiss_edit_status !== "ACTIVE") {
            $('.EditButton').remove();
          }
          if (res.permiss_delete_status !== "ACTIVE") {
            $('.DeleteButton').remove();
          }
          if (res.permiss_add_status !== "ACTIVE") {
            $('#AddNewId').remove();
          }
          if (res.permiss_read_status !== "ACTIVE") {
            $('.ViewButton').remove();
          }
          if (res.permiss_upload_status !== "ACTIVE") {
            $('.UploadComponent').remove();
          }

        } else {
          MessageBox.ErrorMessage(response.code, response.description);
          $('.button-feature').remove();

        }

      },
      error: function (xhr, status, error) {

        console.error(xhr.responseText);
        if (xhr.status === 401) {
          alert("Session expired. Redirecting to login page.");
          window.location.href = window.location.origin + '/Auth/LoginBasic';
          localStorage.removeItem("token");
        }
        alert('Error in form submission');
        $('.button-feature').remove();
      }
    });


  }
});
