CREATE DATABASE PartyHosting;
-- Create Users Table

use PartyHosting;
CREATE TABLE Users (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(255) NOT NULL,
    Email VARCHAR(255) NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    unique(username),
    unique(email)
);

-- Create Party Table
CREATE TABLE Parties (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Title VARCHAR(255) NOT NULL,
    Description TEXT NOT NULL,
    PartyDate DATETIME NOT NULL,
    Seats INT NOT NULL
);

-- Create PartyAttendee Table (Many-to-many relationship)
CREATE TABLE PartyAttendees (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    PartyId INT NOT NULL,
    UserId INT NOT NULL,
    FOREIGN KEY (PartyId) REFERENCES Party(Id),
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);


-- party table formed-- 
drop table parties;
RENAME TABLE partyattendees TO PartyAttendee;

delimiter &&
create trigger reduce_seat_on_insert
after insert on PartyAttendee
for each row
begin
update party
set seats = seats - 1
where id = new.PartyId;

end &&
delimiter ;

delimiter &&
create trigger increase_seats_on_delete
after delete on PartyAttendee
for each row
begin
update party
set seats = seats + 1
where id = Old.PartyId;
end &&
delimiter ;
show triggers;

alter table party add created_by int;


