
//See following post
//https://stackoverflow.com/questions/30350680/how-to-send-authorization-header-with-a-request-in-swagger-ui?rq=1
//this version doesn't get the token so it was a starting point

//This is part one of the process, used to show how to replace ApiKey textbox with username and password
//see other javascript for the ajax function which calls gettoken method with username and password to get
//bearer token

//***************************************************************
// BE SURE TO CHANGE PROPERTIES OF THIS FILE TO EMBEDDED RESOURCE
//***************************************************************



(function () {
    $(function () {
        var basicAuthUI =
            '<div class="input"><input placeholder="username" id="input_username" name="username" type="text" size="10"></div>' +
            '<div class="input"><input placeholder="password" id="input_password" name="password" type="password" size="10"></div>';
        $(basicAuthUI).insertBefore('#api_selector div.input:last-child');
        $("#explore").hide();
        $("#input_apiKey").hide();
        $('#input_username').change(addAuthorization);
        $('#input_password').change(addAuthorization);
    });

    function addAuthorization() {
        var username = $('#input_username').val();
        var password = $('#input_password').val();
        if (username && username.trim() != "" &&  password &&  password.trim() != "") {
            var basicAuth = new SwaggerClient.PasswordAuthorization('basic', username, password);
            window.swaggerUi.api.clientAuthorizations.add("basicAuth", basicAuth);
            console.log("authorization added: username = " + username + ", password = " + password);
        }
    }
})();

//Used the following to ensure that the file actually loaded.

//$(document).ready(function () {
//    alert("Hello from custom JavaScript file.");
//});