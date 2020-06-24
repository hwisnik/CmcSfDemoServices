

//Final version of javascript - displays textboxes to enter username and password
//Gets the token and by adding the token to clientAuthorizations, SWAGGER will add the token to every header that is sent

//See article here:  
//https://stevemichelotti.com/customize-authentication-header-in-swaggerui-using-swashbuckle/
//See Answer 1 bottom of following post:
//https://stackoverflow.com/questions/38906224/swagger-ui-pass-custom-authorization-header?rq=1

//Function replaces the ApiKey textbox with username and input
//When user enters data in both textboxes, function performs ajax call to get a token and
//populates the clientAuthorizations property.  Note that you need to remove AddAuthorization header class (in Swagger folder)
//as the code automatically populates the clientAuthorizations property so you don't want to show it in the UI (which then
//requires that user input the bearer token)


//***************************************************************
// BE SURE TO CHANGE PROPERTIES OF THIS FILE TO EMBEDDED RESOURCE
//***************************************************************


(function () {
    $(function () {
        if (document.getElementsByName('username').length <= 0) {

            var basicAuthUI =
                '<div class="input"><input placeholder="username" id="input_username" name="username" type="text" size="10"></div>' +
                '<div class="input"><input placeholder="password" id="input_password" name="password" type="password" size="10"></div>';
            $(basicAuthUI).insertBefore('#api_selector div.input:last-child');
        }
        $("#explore").hide();
        $("#input_apiKey").hide();
        $('#input_username').change(addAuthorization);
        $('#input_password').change(addAuthorization);
    });

    function addAuthorization() {
        //Need to change token endpoint depending on where the service is hosted  (Token generator and service on same host)
        //Only configured for local dev env (Localhost) & DevDeploy on hq-devweb1 as erms2dev.cmcenergy.com

        //var currUrl = document.URL + "token";


        var currUrl = "https://erms2dev.cmcenergy.com/CmcSfDemoServices/token";
        if (document.URL.indexOf("localhost") !== -1) {
            currUrl = "https://localhost/CmcSfDemoServices/token";
        }
        var username = $('#input_username').val();
        var password = $('#input_password').val();
        if (username && username.trim() != "" && password && password.trim() != "") {
            $.ajax({
                url: currUrl, //"https://localhost/CmcSfDemoServices/token",
                type: "post",
                contenttype: 'x-www-form-urlencoded',
                data: "grant_type=password&username=" + username + "&password=" + password,
                success: function (response) {

                    var bearerToken = "Bearer " + response.access_token;
                    window.swaggerUi.api.clientAuthorizations.remove('api_key');
                    var apiKeyAuth = new SwaggerClient.ApiKeyAuthorization("Authorization", bearerToken, "header");
                    window.swaggerUi.api.clientAuthorizations.add('oauth2', apiKeyAuth);
                    alert("Login Succesful!");

                },
                error: function (xhr, ajaxoptions, thrownerror) {
                    alert("Login failed!");
                }
            });
        }
    }
})();
