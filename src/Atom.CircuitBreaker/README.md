Atom.CircuitBreaker
=======

Atom.CircuitBreaker is a fault tolerance and isolation library that employs **timeouts, bulkheads, and circuit breakers** to make network and other dependency calls resilient against failure. 
It's fork of Hudl Mjolnir but convert to use DotNet Core.
Mjolnir is modeled after Netflix's awesome [Hystrix](https://github.com/Netflix/Hystrix) library. Some components are ports, but much of it has been written using C#- and .NET-specific features (e.g. `async/await`, `CancellationToken`s).

See the [Wiki](https://github.com/hudl/Mjolnir/wiki) for more details and documentation.