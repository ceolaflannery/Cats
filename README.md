# Cats

## Installation

Requires [.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1) to run.

### Run
```sh
cd Pets
dotnet run
```

### Test
```sh
cd Pets
dotnet test
```

## Assumptions
- All business logic assumptions are documented in unit test names
- The purpose of this service is focussed around Pets as opposed to the people
- No results being returned is not an expected result
- May want to display data in different formats and for different pet types in future


## Notes
- Timeboxed the work on this
- In the interest of time I implemented some unit tests to demonstrate my skill and style but left some not implemented but detailed all of the test cases in the test names


## What I could have done with more time
- Implemented logging todos
- Implemented unfinished unit tests
- Pull out reusable setup and verify methods in tests to somewhere common

## Another possible approach
Get more specific on the domain by mapping the response to PetOwner and Cat types. 
This would allow separating the filtering from the grouping/projection happening in the Linq statement but would complicate things a little when extending the solution. I would also require more iteration through the collection and maping so whether to go this way would depend on the trade offs for the project e.g. How critical is the speed, How critical is it to be able to extend the solution rapidly, what direction the solution might be extended etc
