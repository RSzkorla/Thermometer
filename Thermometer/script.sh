#!/usr/bin/expect -f
spawn minicom -D /dev/ttyS0\r"
send "AT\r"
expect
send "AT + CMGF = 1\r"
expect
send "AT+CMGS=\"+48883984162\"\r"
expect
send "Alert!!! Temperature is out of allowed range!"
expect
send " 32"
