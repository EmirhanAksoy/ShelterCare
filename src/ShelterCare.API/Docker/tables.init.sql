CREATE TABLE IF NOT EXISTS Shelters (
    Id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    Name VARCHAR(255) NOT NULL,
    OwnerFullName VARCHAR(255),
    FoundationDate TIMESTAMP,
    TotalAreaInSquareMeters DOUBLE PRECISION,
    Address TEXT,
    Website VARCHAR(255),
    IsActive BOOLEAN NOT NULL,
    CreateDate TIMESTAMP NOT NULL,
    CreateUserId UUID NOT NULL,
    UpdateDate TIMESTAMP,
    UpdateUserId UUID,
     unique(Name) 
);

CREATE TABLE IF NOT EXISTS AnimalSpecies (
    Id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    IsActive BOOLEAN,
    CreateDate TIMESTAMP,
    CreateUserId UUID,
    UpdateDate TIMESTAMP,
    UpdateUserId UUID,
    Name VARCHAR(255),
    unique(Name)  
);

INSERT INTO AnimalSpecies (name) VALUES ('cat')
ON CONFLICT (name) DO NOTHING;

INSERT INTO AnimalSpecies (name) VALUES ('dog')
ON CONFLICT (name) DO NOTHING;

INSERT INTO AnimalSpecies (name) VALUES ('bird')
ON CONFLICT (name) DO NOTHING;

CREATE TABLE IF NOT EXISTS AnimalOwners (
    Id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    Fullname VARCHAR(255) NOT NULL,
    NationalId TEXT,
    EmailAddress TEXT,
    PhoneNumber TEXT,
    IsActive BOOLEAN,
    CreateDate TIMESTAMP,
    CreateUserId UUID,
    UpdateDate TIMESTAMP,
    UpdateUserId UUID,
     unique(NationalId) 
);