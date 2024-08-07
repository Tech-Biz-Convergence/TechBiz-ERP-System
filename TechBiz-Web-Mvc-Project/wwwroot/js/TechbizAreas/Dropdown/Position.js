'use strict';
$(document).ready(function () {

  debugger;
  SetPositionDDL();
  function SetPositionDDL() {
    $('#AddFormId [name=position_id]').select2({
      tags: true,
      //dropdownParent: $('#assyPartControlModal'),
      theme: 'bootstrap-5',
      placeholder: 'Select Position',
      //allowClear: true
      ajax: {
        url: config.apiUrl.base + '/Api/Hr/Position/GetPaginate',
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
                id: item.position_id,
                text: item.position_name
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
