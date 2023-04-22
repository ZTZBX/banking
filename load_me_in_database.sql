/*
BAKING ZTZBX SQL SCRIPT
*/

CREATE TABLE economy (
    username varchar(50) NOT NULL,
    `money` int NOT NULL DEFAULT 0,
    vp int NOT NULL DEFAULT 0,
    PRIMARY KEY (username),
    FOREIGN KEY (username) REFERENCES players(username)
)ENGINE=InnoDB;