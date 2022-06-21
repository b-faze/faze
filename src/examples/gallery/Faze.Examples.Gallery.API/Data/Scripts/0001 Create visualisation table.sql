CREATE TABLE visualisation (
    file_id VARCHAR(100) NOT NULL,
    album VARCHAR(1000) NOT NULL,
    pipeline VARCHAR(100) NOT NULL,
    depth INT NOT NULL,
    variation VARCHAR(100) NOT NULL,
    tree_size INT NULL,
    image_size INT NOT NULL,
);

INSERT INTO visualisation (file_id, album, pipeline, depth, variation, tree_size, image_size) VALUES
	('V1 OX Gold 1.png', 'OX', 'OX Gold', 1, 'var 1', 3, 500);