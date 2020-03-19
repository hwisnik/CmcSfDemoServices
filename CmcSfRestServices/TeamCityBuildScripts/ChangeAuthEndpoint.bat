rem Took a differenet approach and this script is no longer being used

rem input parameter from TeamCity Build Step Change Swagger Authorization Endpoint is the TeamCity working directory  (%teamcity.build.workingDir%/CmcSfRestServices)
rem replace AdAuthDevLocahost.js with AdAuthDevDeploy.js

cd %1/CmcSfRestServices/swagger
dir
del AdAuthDevLocahost.js
dir
ren AdAuthDevDeploy.js AdAuthDevLocahost.js
copy AdAuthDevLocahost.js AdAuthDevDeploy.js

dir