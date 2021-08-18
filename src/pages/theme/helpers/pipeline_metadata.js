module.exports = (settings, id) => {
    const baseCodeUrl = "https://github.com/b-faze/faze/tree/master/src/examples/gallery/Faze.Examples.Gallery/";

    var info = settings.pipelineMetadata[id];
    if (!info) return {};

    info.codeUrl = baseCodeUrl + info.relativeCodePath;

    return info;
};
