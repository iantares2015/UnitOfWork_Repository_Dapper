CREATE TABLE Users
(
    Id              SERIAL PRIMARY KEY,
    Name            VARCHAR(50) NOT NULL,
    CurrentBalance REAL
);

CREATE TABLE Transfers
(
    Id             TEXT PRIMARY KEY,
    SourceUserId INT       NOT NULL,
    TargetUserId INT       NOT NULL,
    Amount         REAL      NOT NULL,
    Timestamp      TIMESTAMP NOT NULL
);

CREATE TABLE Products
(
    Id             SERIAL PRIMARY KEY,
    Title          TEXT    NOT NULL,
    Price          REAL      NOT NULL
);

-- Seed data
INSERT INTO Users (Name, CurrentBalance)
VALUES ('User A', 1000.00);
INSERT INTO Users (Name, CurrentBalance)
VALUES ('User B', 1500.00);