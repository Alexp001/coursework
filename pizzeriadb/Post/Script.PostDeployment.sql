insert into roles(nameRole)
values ('user'),
('receiver'),
('pizzaManager'),
('employeeManager'),
('moderator'),
('accountant'),
('clientManager'),
('executor');

insert into users(userLogin, userPassword)
values ('alexp', '1234');

insert into AccountingRoles(userId, roleId)
values(1, 4);

insert into employee(nameOfEmployee, email, phone, salary, employeePosition, address, userId)
values('Alex Pischuk', 'alexp6@gmail.com', '+375292294104', 500, 'superUser', 't. Gomel', 1)