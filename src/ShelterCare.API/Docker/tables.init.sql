CREATE TABLE IF NOT EXISTS Shelters (
    Id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    Name VARCHAR(255) NOT NULL,
    OwnerFullName VARCHAR(255),
    FoundationDate DATE,
    TotalAreaInSquareMeters DOUBLE PRECISION,
    Address TEXT,
    Website VARCHAR(255),
    IsActive BOOLEAN NOT NULL,
    CreateDate TIMESTAMP NOT NULL,
    CreateUserId UUID NOT NULL,
    UpdateDate TIMESTAMP,
    UpdateUserId UUID
);