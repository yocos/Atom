#!/usr/bin/env bash

#exit if any command fails
set -e

artifactsFolder="./artifacts"

if [ -d $artifactsFolder ]; then  
  rm -R $artifactsFolder
fi

dotnet restore

dotnet build src/Atom.Common
dotnet build src/Atom.ServiceDiscovery.Abstractions
dotnet build src/Atom.ServiceDiscovery.Consul
dotnet build samples/µServiceSimple

revision=${TRAVIS_JOB_ID:=1}  
revision=$(printf "%04d" $revision) 