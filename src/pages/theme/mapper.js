module.exports = function(file) {
    var albums = [];

    var pathParts = file.path.split('/');
    var folderPath = pathParts.slice(0, pathParts.length - 1).join('/');
    var albumPath = pathParts.slice(0, pathParts.length - 3).join('/');

    albums.push(folderPath);
    albums.push(albumPath);

    return albums;
  }