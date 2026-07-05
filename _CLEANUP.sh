#!/bin/bash

echo "Cleaning up StudyFlow..."

cd StudyFlow_API
dotnet clean

rm -rf bin obj

cd ../Database
rm -f studyflow.db

rm -f ~/dotnet-install.sh

echo "Done."