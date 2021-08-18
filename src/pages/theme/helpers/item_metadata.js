module.exports = (settings, filename) => {
    var info = settings.itemMetadata[filename];
    if (!info) return {};

    return info;
};
