$file = "$env:LOCALAPPDATA\Google\Chrome\User Data\Default\Preferences"
$content = (Get-Content -Encoding UTF8 $file) -replace '.google.com.hk','.google.com'
[System.IO.File]::WriteAllLines($file, $content)
Write-Output Done.
