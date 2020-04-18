# Cats


<<<<<<< HEAD

# Assumptions
=======
## Installation

Requires [.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1) to run.

```sh
clone
cd Pets
dotnet run
```


## Assumptions
>>>>>>> b91f4ce6816e9cd60be4d64410e8408ebb55ef8c
- All business logic assumptions are documented in unit test names
- The purpose of this service is focussed around Pets as opposed to the people
- No results being returned is not an expected result
- May want to display data in different formats and for different pet types in future


<<<<<<< HEAD
# Notes
=======
## Notes
>>>>>>> b91f4ce6816e9cd60be4d64410e8408ebb55ef8c
- Timeboxed the work on this
- In the interest of time I implemented some unit tests to demonstrate my skill and style but left some not implemented but detailed all of the test cases in the test names


<<<<<<< HEAD
# What I could have done with more time
=======
## What I could have done with more time
>>>>>>> b91f4ce6816e9cd60be4d64410e8408ebb55ef8c
- Implemented logging
- Implemented unfinished unit tests
- Pull out reusable setup and verify methods in tests to somewhere common
- Get more specific on the domain by mapping the response to PetOwner and Cat types. This would allow separating the filtering from the grouping/projection happening in the line statement but would complicate things a little when extending the solution
