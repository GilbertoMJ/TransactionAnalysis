# TransactionAnalysis
## Description
I developed this transaction analysis system for a internet banking legacy system.

At the moment, this is just a backup repository for my code, so it is not executable. I believe it is possible to transform it into a microservice.

It should use it before executing a transaction, as a form of validation. I created an example in the CreateTransfer method in the TransferService class.

The first step is to instantiate the transaction adapter. It is optional, because depending on the structure of the project entities, it may not be necessary.

The second step is to instantiate the transaction analyzer. When creating this class, we need to add all the rules that will be observed for this transaction, depending on the business rules. When instantiating, we send the necessary data for these rules to be instantiated; in this case, only the client plan was necessary.

The third step is to call the AnalyzeTransaction method, which goes through all the rules created in the analyzer. This method returns whether the transaction is blocked and the rules that it broke, and can return the messages to the client and/or log them.
