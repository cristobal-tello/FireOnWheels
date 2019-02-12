FireOnWheels Pluralsight personal sample

Found some code on https://github.com/jeffgabriel/FireOnWheels


Shared assembly
IRegisterOrder
IOrderRegistered

Order registration service
RegisterOrder: IRegisterOrder
OrderRegistered : IOrderRegistered

Notification service
OrderRegistered: IOrderRegistered


Using Web interface I place an order and Register Order is done send a message to RegisterOrder. From there a OrderRegistered message is send to Notification service.

* RabbitMQ localhost
http://localhost:15672/#/
User and password is 'guest'

* RabbitMQ Managament Plugin
From C:\Program Files\RabbitMQ Server\rabbitmq_server-3.7.11\sbin, run:

rabbitmq-plugins enable rabbitmq_management


TO-DO
====

1) Configure nuget to use only one folder for all projects
2) Remove warning to allow use 'var' instead full type while you are coding
