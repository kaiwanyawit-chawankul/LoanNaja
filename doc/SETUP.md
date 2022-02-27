# Loan-naja-tdd-workshop

## Structures
### loan
Backend application, exposes APIs for loan domain (e.g. taking a loan, viewing loans belonged to an account), containing 2 modules:
- **LoanNaja**: Web application
- **LoanNaja.Core**: Loan service APIs.
- **LoanNaja.xxxTest and LoanNaja.Spec**: Tests

### db
Database module, used by docker

## Setup
Setup for project for [Visual Studio, VsCode](doc/SETUP.md)

## Requirements
`dotnet`

`docker` latest version

`docker-compose` [compatible](https://docs.docker.com/compose/compose-file/) with version 3.7 of compose files

For people who don't have DDoM please visit [this](SETUP-WITHOUT-DD.md)

## How to test
Run `./build.sh` from the main directory to build all modules

## How to build & run
Run `./build.sh` from the main directory to build all modules

Next, run `./start.sh` to run all of them.

## Troubleshooting
In any case you'd like to clean docker containers and LoansLah docker's images run `./reset.sh`
