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
    UpdateUserId UUID
);

CREATE TABLE IF NOT EXISTS AnimalSpecies (
    Id UUID PRIMARY KEY,
    IsActive BOOLEAN,
    CreateDate TIMESTAMP,
    CreateUserId UUID,
    UpdateDate TIMESTAMP,
    UpdateUserId UUID,
    Name VARCHAR(255) 
);

INSERT INTO Animals (name) VALUES ('cat')
ON CONFLICT (name) DO NOTHING;

INSERT INTO Animals (name) VALUES ('dog')
ON CONFLICT (name) DO NOTHING;

INSERT INTO Animals (name) VALUES ('bird')
ON CONFLICT (name) DO NOTHING;