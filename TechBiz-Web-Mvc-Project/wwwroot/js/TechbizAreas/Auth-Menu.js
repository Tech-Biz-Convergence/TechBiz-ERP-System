/**
 * Account Settings - Account
 */

'use strict';
$(document).ready(function () {

  // Function to handle click event on menu links to call your custom function
  $(document).on('click', '.menu-link', function (event) {

    // Prevent default link action
    event.preventDefault();

    // Get token, username, and permId from localStorage
    const token = localStorage.getItem("token");
    const username = localStorage.getItem("user_name");
    if (!token || !username ) {
      // Redirect to login page if any of the values are missing
      window.location.href = '/Auth/LoginBasic';
      alert('Missing authentication details');
      return;
    }

    // Get the href attribute of the clicked link
    var hrefValue = $(this).attr('href');
    console.log("Original Link:", hrefValue);

    // Append user_name and token to the URL
    hrefValue += (hrefValue.includes('?') ? '&' : '?') + 'user_name=' + encodeURIComponent(username) + '&token=' + encodeURIComponent(token);
    console.log("Modified Link:", hrefValue);

    // Navigate to the modified href value
    window.location.href = hrefValue;
  });

  $(document).on('click', '#logoutId', function () {
    console.log("Log out");
    window.location.href = window.location.origin + '/Auth/LoginBasic';
  });

});
