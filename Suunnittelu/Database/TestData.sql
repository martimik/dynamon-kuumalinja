-- TESTIDATA

INSERT INTO userClass (description) VALUES ("Admin"),("NormalUser");
INSERT INTO channel (channelName, channelPassword) VALUES ("TestChannel1","TestChannel1"),("TestChannel2","TestChannel2");
INSERT INTO user (username, password, classID) VALUES ("admin","admin", 1), ("testUser1","testUser1", 2), ("testUser2","testUser2", 2);
INSERT INTO moderator (userID, channelID) VALUES (2,1), (3,2);
INSERT INTO message (channelID, userID, timeStamp, content) VALUES 
(1,2, CURTIME(), "Lorem ipsum dolor sit amet"), 
(1,3, CURTIME(), "consectetur adipiscing elit"), 
(1,2, CURTIME(), "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua"), 
(1,3, CURTIME(), "Ut enim ad minim veniam"),
(2,3, CURTIME(), " quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat"),
(2,3, CURTIME(), "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur"),
(2,3, CURTIME(), "Excepteur sint occaecat cupidatat non proident"),
(2,1, CURTIME(), "Ei spämmitä sitä lorem ipsumia");