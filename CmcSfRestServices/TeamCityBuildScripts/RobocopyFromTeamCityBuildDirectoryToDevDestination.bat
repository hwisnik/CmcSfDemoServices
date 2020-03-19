rem input parameter from TeamCity Build Step Copy To Folder is the TeamCity working directory/CmcSfRestServices  (%teamcity.build.workingDir%/CmcSfRestServices)

rem copy top level (CmcSfRestServices) config files and web pages
robocopy %1 \\HQ-DEVWEB1\CmcSfRestServices  /xf "*.cs*" "*.*proj"

rem copy files from bin directory except debug files
robocopy %1/bin \\HQ-DEVWEB1\CmcSfRestServices\bin /e /xf "*.pdb" 

rem copy EntloggerConfig folder which is at the top level (CmcSfRestServices) level
robocopy %1/EntLoggerConfig \\HQ-DEVWEB1\CmcSfRestServices\EntloggerConfig 

set/A errlev="%ERRORLEVEL% & 24"
rem exit batch file with errorlevel so SQL job can succeed or fail appropriately
exit/B %errlev%

rem The SET command lets you use expressions when you use the /A switch.  So I set an environment variable "errlev" to a bitwise AND with the ERRORLEVEL value.
rem Robocopy's exit codes use a bitmap/bitmask to specify its exit status.  The bits for 1, 2, and 4 do not indicate any kind of failure, but 8 and 16 do.  
rem So by adding 16 + 8 to get 24, and doing a bitwise AND, I suppress any of the other bits that might be set, and allow either or both of the error bits to pass.
rem The next step is to use the EXIT command with the /B switch to set a new ERRORLEVEL value, using the "errlev" variable.  
ren This will now return zero (unless Robocopy had real errors) and allow your TeamCity job step to report success.

rem echo "robocopy done. this will make the build step succeed always" replaced this statment which swallows all errors with set/A etc. method