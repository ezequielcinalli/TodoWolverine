# TodoWolverine

A simple PoC of the [Wolverine](https://wolverine.netlify.app/) library building a very minimalistic todo list api.

Uses [Marten](https://martendb.io/) as the data store with event sourcing paradigm.

The idea is use a CQRS approach but instead of the classic approach with mediator pattern libraries as MediatR or
Mediator.SourceGenerator using wolverine.

For integration testing use [Alba](https://jasperfx.github.io/alba/) which have great integration with marten and wolverine.

# Setup

Have a local postgres instance running and create a database called `Wolverine_Todo_List`. See appsettings.json file for
connection string for parameters (Marten section).
