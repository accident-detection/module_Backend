#!/bin/bash

while true; do
    var=$(tail -n 1 /var/log/system.log)
    log="{\"logmsg\": \"$var\"}"
    echo "$log" | curl -H "Content-Type: application/json" -d @- http://localhost:5000/api/events
    sleep 5
done
