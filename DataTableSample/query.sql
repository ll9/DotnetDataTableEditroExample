CREATE TABLE IF NOT EXISTS features(
    id integer primary key,
    name varchar(30),
    age decimal
);

INSERT INTO features(name, age) values
    ("Peter", 22),
    ("Hans", 14),
    ("Josef", 28);