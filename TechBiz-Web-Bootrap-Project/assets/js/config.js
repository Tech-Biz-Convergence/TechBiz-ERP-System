/**
 * Config
 * -------------------------------------------------------------------------------------
 * ! IMPORTANT: Make sure you clear the browser local storage In order to see the config changes in the template.
 * ! To clear local storage: (https://www.leadshook.com/help/how-to-clear-local-storage-in-google-chrome-browser/).
 */

'use strict';

// JS global variables
let config = {
  colors: {
    primary: '#696cff',
    secondary: '#8592a3',
    success: '#71dd37',
    info: '#03c3ec',
    warning: '#ffab00',
    danger: '#ff3e1d',
    dark: '#233446',
    black: '#000',
    white: '#fff',
    body: '#f4f5fb',
    headingColor: '#566a7f',
    axisColor: '#a1acb8',
    borderColor: '#eceef1'
  },
  apiUrl: {
    base: 'https://localhost:7035'
  }
};




//Custome function by game
$.fn.serializeObject = function () {
  var result = {};
  var formData = this.serializeArray();
  $.each(formData, function () {
    if (result[this.name]) {
      if (!result[this.name].push) {
        result[this.name] = [result[this.name]];
      }
      result[this.name].push(this.value || '');
    } else {
      result[this.name] = this.value || '';
    }
  });
  return result;
}

class MessageBox {
  static SuccessMessage(code, description) {
    return Swal.fire({
      title: code,
      text: description,
      icon: "success"
    });
  }

  static ErrorMessage(code, description) {
    return Swal.fire({
      title: code,
      text: description,
      icon: "error"
    });
    
  }

  static ConfirmDeletedMessage(message, callback) {
    return Swal.fire({
      title: "Are you sure?",
      text: message,
      icon: "warning",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "Yes, delete it!"
    }).then((result) => {
      if (result.isConfirmed) {
        if (callback) {
          return callback().then((success) => {
            if (success) {
              Swal.fire({
                title: "Deleted!",
                text: "Your data has been deleted.",
                icon: "success"
              });
            } else {
              Swal.fire({
                title: "Error!",
                text: "There was a problem deleting your data.",
                icon: "error"
              });
            }
            return success;
          }).catch((error) => {
            Swal.fire({
              title: "Error!",
              text: "There was a problem deleting your data.",
              icon: "error"
            });
            return false;
          });
        }
      }
      return false;
    });
  }
}