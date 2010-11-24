CREATE TABLE short_url (
    id  VARCHAR(6) NOT NULL PRIMARY KEY,
    url TEXT NOT NULL,
    added DATETIME NOT NULL,
    last_visited DATETIME
);

CREATE TABLE tracker_data (
    url_id VARCHAR(6) NOT NULL,
    referer TEXT NOT NULL,
    ip VARCHAR(15),
    visited DATETIME
);

CREATE INDEX "id" ON "short_url" ("id");