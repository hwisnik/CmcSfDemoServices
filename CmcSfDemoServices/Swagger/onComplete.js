

//Example of showing the bearer token in a textbox but it requires the user
//to copy and paste into the Authorization textbox (Required in each method).
//In ordr to show the Authorization textbox you need to add and Authorization header
//See unused code in Swagger folder


//***************************************************************
// BE SURE TO CHANGE PROPERTIES OF THIS FILE TO EMBEDDED RESOURCE
//***************************************************************



$('#input_apiKey').change(function () {
    var key = $('#input_apiKey')[0].value;
    var credentials = key.split(':');
    $.ajax({
        url: "https://localhost/CmcSfDemoServices/token",
        type: "post",
        contenttype: 'x-www-form-urlencoded',
        data: "grant_type=password&username=" + credentials[0] + "&password=" + credentials[1],
        success: function (response) {

            var bearerToken = "Bearer " + response.access_token;

            window.swaggerUi.api.clientAuthorizations.remove('api_key');

            var apiKeyAuth = new SwaggerClient.ApiKeyAuthorization("Authorization", bearerToken, "header");

            window.swaggerUi.api.clientAuthorizations.add('oauth2', apiKeyAuth);

            $("#explore").hide();
            $("#input_apiKey").hide();

             if (document.getElementsByName('txtBearerToken').length <= 0 ) {
                var basicAuthUI =  $(basicAuthUI).insertAfter('#api_selector div.input:last-child');
            }

            var textbox = document.getElementsByName('txtBearerToken')[0]
            textbox.value = bearerToken;

            alert("Login Succesfull!");

        },
        error: function (xhr, ajaxoptions, thrownerror) {
            alert("Login failed!");
            $("#txtBearerToken").hide();
        }
    });
});