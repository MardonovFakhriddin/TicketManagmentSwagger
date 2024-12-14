CREATE TABLE Tickets
(
    TicketId      INT PRIMARY KEY,
    Type          VARCHAR(50),
    Title         VARCHAR(150),
    Price         DECIMAL(10, 2),
    EventDateTime TIMESTAMP
);

CREATE TABLE Customers
(
    CustomerId INT PRIMARY KEY,
    FullName   VARCHAR(150),
    Email      VARCHAR(150),
    Phone      VARCHAR(20)
);

CREATE TABLE Purchases
(
    PurchaseId       INT PRIMARY KEY,
    TicketId         INT,
    CustomerId       INT,
    PurchaseDateTime TIMESTAMP,
    Quantity         INT,
    TotalPrice       DECIMAL(10, 2),
    FOREIGN KEY (TicketId) REFERENCES Tickets (TicketId),
    FOREIGN KEY (CustomerId) REFERENCES Customers (CustomerId)
);

CREATE TABLE Locations
(
    LocationId   INT PRIMARY KEY,
    City         VARCHAR(100),
    Address      VARCHAR(200),
    LocationType VARCHAR(50)
);

CREATE TABLE TicketLocations
(
    TicketId   INT,
    LocationId INT,
    FOREIGN KEY (TicketId) REFERENCES Tickets (TicketId),
    FOREIGN KEY (LocationId) REFERENCES Locations (LocationId),
    PRIMARY KEY (TicketId, LocationId)
);


INSERT INTO Tickets (TicketId, Type, Title, Price, EventDateTime)
VALUES
    (6, 'Flight', 'Flight Los Angeles - Tokyo', 700.00, '2024-12-18 10:00:00'),
    (7, 'Train', 'Train Berlin - Munich', 120.00, '2024-12-26 12:00:00'),
    (8, 'Bus', 'Bus Paris - London', 60.00, '2024-12-23 14:00:00'),
    (9, 'Event', 'Theater in Berlin', 90.00, '2024-12-29 20:00:00'),
    (10, 'Flight', 'Flight Sydney - New York', 950.00, '2024-12-30 23:00:00');

INSERT INTO Customers (CustomerId, FullName, Email, Phone)
VALUES
    (4, 'David Johnson', 'david.johnson@example.com', '+2233445566'),
    (5, 'Mary Williams', 'mary.williams@example.com', '+2233445567'),
    (6, 'George Clark', 'george.clark@example.com', '+2233445568');

INSERT INTO Purchases (PurchaseId, TicketId, CustomerId, PurchaseDateTime, Quantity, TotalPrice)
VALUES
    (6, 6, 4, '2024-12-15 10:00:00', 2, 1400.00),
    (7, 7, 5, '2024-12-16 11:15:00', 1, 120.00),
    (8, 8, 6, '2024-12-17 09:30:00', 4, 240.00),
    (9, 9, 4, '2024-12-19 18:00:00', 1, 90.00),
    (10, 10, 5, '2024-12-20 20:45:00', 1, 950.00);

INSERT INTO Locations (LocationId, City, Address, LocationType)
VALUES
    (6, 'Los Angeles', 'Los Angeles International Airport', 'Airport'),
    (7, 'Munich', 'Munich Central Station', 'Railway Station'),
    (8, 'London', 'Victoria Coach Station', 'Bus Station'),
    (9, 'Berlin', 'Berlin Opera House', 'Event Hall'),
    (10, 'Sydney', 'Sydney International Airport', 'Airport');

INSERT INTO TicketLocations (TicketId, LocationId)
VALUES
    (6, 6),
    (6, 10),
    (7, 7),
    (8, 8),
    (9, 9),
    (10, 10);
