module.exports = (context) => {
    var album = context.album;
    var breadcrumbs = context.breadcrumbs;
    var homeAlbum = context.gallery.home;

    var depthToItem = 1;
    var currAlbum = album;
    while (currAlbum.albums.length > 0) {
        depthToItem++;
        currAlbum = currAlbum.albums[0];
    }

    var fullDepth = album.depth + depthToItem;
    var albumRootDepth = fullDepth - 3;
    var pipelineAlbumDepth = fullDepth - 2;
    var variationAlbumDepth = fullDepth - 1;
    var breadcrumbData = [];

    var currBreadcrumbAlbum = homeAlbum;
    for (var i in breadcrumbs) {
        currBreadcrumbAlbum = breadcrumbs[i];
        breadcrumbData.push({
            title: currBreadcrumbAlbum.title,
            url: currBreadcrumbAlbum.url,
            hasNext: true,
            cssClass: getBreadcrumbCssClass(currBreadcrumbAlbum.depth, albumRootDepth)
        });
    }
    breadcrumbData.push({
        title: album.title,
        url: album.url,
        hasNext: false,
        cssClass: getBreadcrumbCssClass(album.depth, albumRootDepth)
    });

    return {
        id: album.title,
        breadcrumbs: breadcrumbData,
        isAlbumRoot: album.depth === albumRootDepth,
        isPipeline: album.depth === pipelineAlbumDepth,
        isVariation: album.depth === variationAlbumDepth
    };
};

function getBreadcrumbCssClass(albumDepth, rootDepth) {
    var relativeDepth = albumDepth - rootDepth;
    return relativeDepth === 1
            ? "breadcrumb-badge breadcrumb-pipeline"
            : relativeDepth === 2
            ? "breadcrumb-badge breadcrumb-variation"
            : "";
}
