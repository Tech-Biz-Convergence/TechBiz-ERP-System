/**
 * Account Settings - Account
 */

'use strict';
$(document).ready(function() {



    //============================================================================
    // ZONE : RUN INIT
    // create date   : 20240619 
    // create by     : Game
    // detail        :
    //============================================================================
   
    const token = localStorage.getItem("token");    
    const username = localStorage.getItem("user_name");        
    if (!token || !username) {
        window.location.href= 'login.html';
    }       

    console.log("username"+username);
    var addStatus = true;
    var company_id;
    ShowForm(); 
   
    //============================================================================
    // ZONE : SUBMIT
    // create date   : 20240619 
    // create by     : Game
    // detail        :
    //============================================================================
    $('#AddFormId').submit(async function(event) {
        event.preventDefault();

        var formData = $(this).serializeObject (); 
        console.log(formData);        
        AddFromSubmit(formData);
        
    });

    $('#UploadFormId').submit(async function(event) {
        event.preventDefault();

        var formData = new FormData(this); 
        console.log(formData);
        
        ShowUploadListDataTable(formData)
        
    });
    
    //============================================================================
    // ZONE : EVENT CLICK
    // create date   : 20240619 
    // create by     : Game
    // detail        :
    //============================================================================

    $(document).on('click', '#logoutId', function() {
        console.log("Log out");
        window.location.href = 'login.html'; 
    });            
               

    //============================================================================
    // ZONE : CUSTOM FUNCTION
    // create date   : 20240619 
    // create by     : Game
    // detail        :
    //============================================================================
            
    async function  AddFromSubmit(formData)
    {
        var type;
        var url;
        var jsonData = JSON.parse(formData);       
             
        //check insert or update
        if (jsonData.id == '0') {
            //insert 
            
            //formData.create_by = username;
            var type = 'POST';
            var url =config.apiUrl.base+'/api/hr/CompanyInfo/addnew';
        }else{
            //update
            
            //formData.update_by = username;   
            //formData.company_id = company_id;           
            type = 'PUT';
            url = config.apiUrl.base+'/api/hr/CompanyInfo/update';                                  
        }        
        
        await $.ajax({
            url: url,
            type: type,
            contentType: 'application/json; charset=utf-8',
            data: formData,
            headers: {"Authorization": "Bearer " + token},
            success: function(response) {
                console.log(response);
                
                if(response.status == true){
                    //$('#AddFormId')[0].reset();
                    MessageBox.SuccessMessage(response.code,response.description);
                }else{
                    alert("Error Code : " + response.code +" Error Description " +response.description);
                    MessageBox.ErrorMessage(response.code,response.description);
                }               
            },
            error: function(xhr, status, error) {
                
                console.error(xhr.responseText);
                if (xhr.status === 401) {
                    alert("Session expired. Redirecting to login page.");
                    window.location.href = 'login.html'; 
                    localStorage.removeItem("token");
                }
                alert('Error in form submission');
            }
        });
        
        // $('.SlideTabSearch').hide();
        // $('#AreaSearchId').show();

        // Page.mainTableControl.ajax.reload();

        location.reload();

    }


    async function ShowForm()
    {
        await $.ajax({
            url: config.apiUrl.base+'/api/hr/CompanyInfo/get',
            type: 'GET',
            contentType: 'application/json; charset=utf-8',
            // data: {
            //     id : id
            // },
            headers: {"Authorization": "Bearer " + token},
            success: function(response) {
                console.log(response);           
                
                addStatus = true;
                
                $('#AddFormId [name=create_by]').val(username);                

                if(response.status == true){
                    addStatus = false;
                    var res = response.data[0];
                    
                    company_id = res.company_id;                                        
                    $('#AddFormId [name=id]').val(res.company_id); 
                    $('#AddFormId [name=company_id]').val(res.company_id);   
                    $('#AddFormId [name=update_by]').val(username);                 
                    $('#AddFormId [name=update_by_show]').val(res.update_by);
                    $('#AddFormId [name=company_tax_id]').val(res.company_tax_id);
                    $('#AddFormId [name=company_name_th]').val(res.company_name_th);
                    $('#AddFormId [name=company_name_en]').val(res.company_name_en);
                    $('#AddFormId [name=company_address]').val(res.company_address);
                    $('#AddFormId [name=company_city]').val(res.company_city);
                    $('#AddFormId [name=company_country]').val(res.company_country);
                    $('#AddFormId [name=company_province]').val(res.company_province);
                    $('#AddFormId [name=company_postal_code]').val(res.company_postal_code);
                    $('#AddFormId [name=company_phone_no]').val(res.company_phone_no);
                    $('#AddFormId [name=company_mobile_no]').val(res.company_mobile_no);
                    $('#AddFormId [name=company_fax_no]').val(res.company_fax_no);
                    $('#AddFormId [name=company_email]').val(res.company_email);
                    $('#AddFormId [name=company_url]').val(res.company_url);
                    $('#AddFormId [name=create_by_show]').val(res.create_by);
                    $('#AddFormId [name=create_date]').val(res.create_date);
                    $('#AddFormId [name=update_date]').val(res.update_date);

                }else{                    
                    MessageBox.ErrorMessage(response.code,response.description);                    
                }               
            },
            error: function(xhr, status, error) {
                
                console.error(xhr.responseText);
                if (xhr.status === 401) {
                    alert("Session expired. Redirecting to login page.");
                    window.location.href = 'login.html'; 
                    localStorage.removeItem("token");
                }
                alert('Error in form submission');
            }
        });

    }

    function ClearAddNew()
    {
        $('#AddFormId').trigger('reset');
        $('#AddFormId [name=id]').val('0');
        $('#saveId').prop('disabled', false);               
    }    
}); //end ready 


