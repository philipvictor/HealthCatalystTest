
# All test match coded Integration Test in the HealthCatalystUserSearchAPI.IntegrationTests project.

#
#-------------------------------------------------------------------------------------------------------------------
#
# Integration Test - Get all users - Should succeed.
Write-Host "`nTesting Get all users - Should succeed.`n"

$ResponseOne = Invoke-WebRequest "https://localhost:44371/api/Users" -Method GET

Write-Host "Response Code:"  $ResponseOne.StatusCode -ForegroundColor Green
Write-Host "`n"$ResponseOne.Content

Read-Host -Prompt "`nPress Enter to continue"

#
#-------------------------------------------------------------------------------------------------------------------
#
# Integration Test - Get a specific user - Should succeed.

Write-Host "`nTesting Get a specific user - Should succeed.`n"

$ResponseTwo = Invoke-WebRequest "https://localhost:44371/api/Users/2517d71a-0152-48b1-bf16-2ab75be91a6f" -Method GET

Write-Host "Response Code:"  $ResponseTwo.StatusCode -ForegroundColor Green
Write-Host "`n"$ResponseTwo.Content

Read-Host -Prompt "`nPress Enter to continue"

#
#-------------------------------------------------------------------------------------------------------------------
#
# Integration Test - Test the Name search by Interest with no interest provided. - Should fail since Interest name required.

Write-Host "`nTesting the Name search by Interest with no interest provided. - Should fail since Interest name required.`n"

Invoke-WebRequest "https://localhost:44371/api/Users/SearchByInterest" -Method GET

Read-Host -Prompt "`nPress Enter to continue"

#
#-------------------------------------------------------------------------------------------------------------------
#
# Integration Test - Test the Name search using an Interest name. - Should succeed.

Write-Host "`nTesting the Name search using an Interest name. - Should succeed.`n"

$ResponseFour = Invoke-WebRequest "https://localhost:44371/api/Users/SearchByInterest?interestname=soccer" -Method GET

Write-Host "Response Code:"  $ResponseFour.StatusCode -ForegroundColor Green
Write-Host "`n"$ResponseFour.Content

Read-Host -Prompt "`nPress Enter to continue"

#
#-------------------------------------------------------------------------------------------------------------------
#
# Integration Test - Test the Name search using an Interest name and type. - Should succeed.

Write-Host "`nTesting the Name search using an Interest name and type. - Should succeed.`n"

$ResponseFive = Invoke-WebRequest "https://localhost:44371/api/Users/SearchByInterest?interestname=soccer&interesttype=sport" -Method GET

Write-Host "Response Code:"  $ResponseFive.StatusCode -ForegroundColor Green
Write-Host "`n"$ResponseFive.Content

Read-Host -Prompt "`nPress Enter to continue"

#
#-------------------------------------------------------------------------------------------------------------------
#
# Integration Test - Test the Name search using an Interest type. - Should fail since Interest name required.

Write-Host "`nTesting the Name search using an Interest type. - Should fail since Interest name required.`n"

Invoke-WebRequest "https://localhost:44371/api/Users/SearchByInterest?interesttype=sport" -Method GET

Read-Host -Prompt "`nPress Enter to continue"

#
#-------------------------------------------------------------------------------------------------------------------
#
# Integration Test - Test the Name search using no provided name. - Should fail since Last name required.

Write-Host "`nTesting the Name search using no provided name. - Should fail since Last name required.`n"

Invoke-WebRequest "https://localhost:44371/api/Users/SearchByName" -Method GET

Read-Host -Prompt "`nPress Enter to continue"

#
#-------------------------------------------------------------------------------------------------------------------
#
# Integration Test - Test the Name search using Last name. - Should succeed.

Write-Host "`nTesting the Name search using Last name. - Should succeed.`n"

$ResponseEight = Invoke-WebRequest "https://localhost:44371/api/Users/SearchByName?lastname=Handle" -Method GET

Write-Host "Response Code:"  $ResponseEight.StatusCode -ForegroundColor Green
Write-Host "`n"$ResponseEight.Content -ForegroundColor Green

Read-Host -Prompt "`nPress Enter to continue"

#
#-------------------------------------------------------------------------------------------------------------------
#
# Integration Test - Test the Name search using First name. - Should fail since Last name required.

Write-Host "`nTesting the Name search using First name. - Should fail since Last name required.`n"

Invoke-WebRequest "https://localhost:44371/api/Users/SearchByName?firstname=Handle" -Method GET

Read-Host -Prompt "`nPress Enter to continue"

#
#-------------------------------------------------------------------------------------------------------------------
#
# Integration Test - Test the Name search using First and Last name. - Should succeed.

Write-Host "`nTesting the Name search using First and Last name.  - Should succeed.`n"

$ResponseTen = Invoke-WebRequest "https://localhost:44371/api/Users/SearchByName?lastname=Handle&firstname=Chris" -Method GET

Write-Host "Response Code:"  $ResponseTen.StatusCode -ForegroundColor Green
Write-Host "`n"$ResponseTen.Content

Read-Host -Prompt "`nPress Enter to continue"


