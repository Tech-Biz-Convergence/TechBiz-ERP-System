'use strict';
$(document).ready(function () {

  debugger;
  SetRoleDDL();
  function SetRoleDDL() {
    $('#AddFormId [name=role_id]').select2({
      tags: true,
      //dropdownParent: $('#assyPartControlModal'),
      theme: 'bootstrap-5',
      placeholder: 'Select Role',
      //allowClear: true
      ajax: {
        url: config.apiUrl.base + '/Api/Auth/Role/GetPaginate',
        type: 'GET',
        dataType: 'json',
        headers: { "Authorization": "Bearer " + token },
        data: (params) => {

          return {
            searchValue: params.term,  //term Ther current serach term in ther serach box.
            page: params.page || 1,
            pageLimit: 10
          };
        },
        processResults: (data) => {

          return {
            results: data.data.data.map(item => {
              return {
                id: item.role_id,
                text: item.role_name
              };
            }),
            pagination: {
              more: data.data.data.length === 10
              //more: data.nextPage
            }
          };
        }
      },
      language: {
        searching: () => {
          return "Loading...";
        },
        loadingMore: () => {
          return "Loading more...";
        }
      }
    });
  }


});
