-- Kanavan moderaattorien käyttäjänimet

SELECT username FROM user INNER JOIN moderator ON user.userID = moderator.userID INNER JOIN channel ON moderator.channelID = channel.channelID WHERE channel.channelID = 1;

-- Käyttäjien kokonaismäärä

SELECT COUNT(userID) FROM user;

-- Tavallisten käyttäjien kokonaismäärä

SELECT COUNT(userID) FROM user WHERE classID != 1;

-- Kanavalle lähetettyjen viestien määrä

SELECT COUNT(MessageID) FROM message WHERE channelID = 1;
