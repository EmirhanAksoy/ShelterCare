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
    UNIQUE(Name)
);

INSERT INTO Shelters (
    Name,
    OwnerFullName,
    FoundationDate,
    TotalAreaInSquareMeters,
    Address,
    Website,
    IsActive,
    CreateDate,
    CreateUserId
) VALUES (
    'HappyAnimals',
    'Emirhan Aksoy',
    CURRENT_TIMESTAMP,
    2500,
    'Ankara',
    'www.happyanimals.com',
    true,
    CURRENT_TIMESTAMP,
    uuid_generate_v4()
);

CREATE TABLE IF NOT EXISTS AnimalSpecies (
    Id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    IsActive BOOLEAN,
    CreateDate TIMESTAMP,
    CreateUserId UUID,
    UpdateDate TIMESTAMP,
    UpdateUserId UUID,
    Name VARCHAR(255),
    UNIQUE(Name)
);

INSERT INTO AnimalSpecies (Name) VALUES ('cat')
ON CONFLICT (Name) DO NOTHING;

INSERT INTO AnimalSpecies (Name) VALUES ('dog')
ON CONFLICT (Name) DO NOTHING;

INSERT INTO AnimalSpecies (Name) VALUES ('bird')
ON CONFLICT (Name) DO NOTHING;

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
    UpdateUserId UUID
);

INSERT INTO AnimalOwners (
    Fullname,
    NationalId,
    EmailAddress,
    PhoneNumber,
    IsActive,
    CreateDate,
    CreateUserId
) VALUES (
    'Emirhan Aksoy',
    '1234',
    'emirhan.aksoy@outlook.com.tr',
    '123456789',
    true,
    CURRENT_TIMESTAMP,
    uuid_generate_v4()
);

CREATE TABLE IF NOT EXISTS Animal (
    Id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    ShelterId UUID,
    AnimalSpecieId UUID,
    OwnerId UUID,
    Name VARCHAR(255) NOT NULL,
    UniqueIdentifier VARCHAR(255) NOT NULL,
    DateOfBirth TIMESTAMP,
    JoiningDate TIMESTAMP,
    IsNeutered BOOLEAN,
    IsDisabled BOOLEAN,
    IsActive BOOLEAN,
    CreateDate TIMESTAMP,
    CreateUserId UUID,
    UpdateDate TIMESTAMP,
    UpdateUserId UUID
);

WITH shelter_info AS (
    SELECT Id FROM Shelters WHERE Name = 'HappyAnimals'
),
animalspecie_info AS (
    SELECT Id FROM AnimalSpecies WHERE Name = 'cat'
),
owner_info AS (
    SELECT Id FROM AnimalOwners WHERE Fullname = 'Emirhan Aksoy'
)

INSERT INTO Animal (
    ShelterId,
    AnimalSpecieId,
    OwnerId,
    Name,
    UniqueIdentifier,
    DateOfBirth,
    JoiningDate,
    IsNeutered,
    IsDisabled,
    IsActive,
    CreateDate,
    CreateUserId
) 
SELECT
    (SELECT Id FROM shelter_info),
    (SELECT Id FROM animalspecie_info),
    (SELECT Id FROM owner_info),
    'LOKI',
    '1234-LOKI',
    '2022-01-01',
    CURRENT_TIMESTAMP,
    true,
    false,
    true,
    CURRENT_TIMESTAMP,
    uuid_generate_v4();