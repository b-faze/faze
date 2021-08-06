module.exports = (settings, filename, property) => {
    var info = settings.imageData[filename];
    if (!info) return "no info";

    return info[property] || "no property: " + property;
};
