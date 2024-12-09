@echo off
REM Create directory structure
mkdir C:\pki\ca\certs C:\pki\ca\crl C:\pki\ca\newcerts C:\pki\ca\private
icacls C:\pki\ca\private /grant:r "%USERNAME%":(OI)(CI)F
echo. > C:\pki\ca\index.txt
echo 1000 > C:\pki\ca\serial

REM Generate root CA key
openssl genrsa -out "C:\pki\ca\private\ca_root_key.pem" 4096

REM Generate root CA certificate
openssl req -new -x509 -days 3650 ^
    -key "C:\pki\ca\private\ca_root_key.pem" ^
    -out "C:\pki\ca\certs\ca_root_cert.pem" ^
    -subj "/C=US/ST=Internal/L=PKI/O=Organization/OU=IT/CN=Internal Root CA"

REM User certificate generation function
IF "%1"=="gen" (
    IF "%2"=="" (
        echo Usage: script.cmd gen username
        exit /b 1
    )
    openssl genrsa -out "C:\pki\ca\private\%2_key.pem" 2048
    
    openssl req -new ^
        -key "C:\pki\ca\private\%2_key.pem" ^
        -out "C:\pki\ca\certs\%2_csr.pem" ^
        -subj "/C=US/ST=Internal/L=PKI/O=Organization/OU=Users/CN=%2"
    
    openssl x509 -req -days 365 ^
        -in "C:\pki\ca\certs\%2_csr.pem" ^
        -CA "C:\pki\ca\certs\ca_root_cert.pem" ^
        -CAkey "C:\pki\ca\private\ca_root_key.pem" ^
        -set_serial "%RANDOM%" ^
        -out "C:\pki\ca\certs\%2_cert.pem"
)

REM Certificate revocation function
IF "%1"=="rev" (
    IF "%2"=="" (
        echo Usage: script.cmd rev username
        exit /b 1
    )
    openssl ca -revoke "C:\pki\ca\certs\%2_cert.pem" ^
        -keyfile "C:\pki\ca\private\ca_root_key.pem" ^
        -cert "C:\pki\ca\certs\ca_root_cert.pem"
    
    openssl ca -gencrl ^
        -keyfile "C:\pki\ca\private\ca_root_key.pem" ^
        -cert "C:\pki\ca\certs\ca_root_cert.pem" ^
        -out "C:\pki\ca\crl\ca_crl.pem"
)

REM Display root CA certificate details
openssl x509 -in "C:\pki\ca\certs\ca_root_cert.pem" -text -noout
