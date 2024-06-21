/**
 * Account Settings - Account
 */

'use strict';
$(document).ready(function() {
    localStorage.removeItem("token");
    $('#AddFormId').submit(function(event) {
        event.preventDefault(); // ป้องกันการรีเฟรชหน้า

        var formData = $(this).serializeObject (); 
        console.log(formData);
        $.ajax({
            url: config.apiUrl.base+'/api/identity/login',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: formData,
            success: function(response) {
                console.log(response);
                if(response.status == true){
                    localStorage.setItem('token', response.data.token);
                    console.log("Token saved to localStorage: " + response.data.token);
                    MessageBox.SuccessMessage(response.description, response.code).then(() => {
                        window.location.href = 'pages-human-resources-employee-management.html';
                      });
                }else{
                    MessageBox.ErrorMessage(response.description,response.code).then(() => {
                        $('#AddFormId')[0].reset();
                        $('#user_name').focus();
                      });
                }
                
                    
                
            },
            error: function(xhr, status, error) {
                alert("error"+error);
                console.error(xhr.responseText);
            }
        });
    });


    
}); //edn ready 


