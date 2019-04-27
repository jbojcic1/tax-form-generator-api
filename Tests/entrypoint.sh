#!/bin/bash
echo "*** Migrations Started ***"
dotnet ef database update
echo "*** Migrations Ended ***"

echo $(pwd)
cd /app/Tests
echo $(pwd)

echo "*** Running Tests ***"
dotnet test
returnCode=$?
echo "*** Tests Done ***"
echo $returnCode
exit $returnCode