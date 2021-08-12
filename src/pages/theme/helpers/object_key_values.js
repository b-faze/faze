module.exports = (obj) => {
    if (!obj) 
        return [];

    return Object.keys(obj).map(k => {
        return {
            key: k, 
            value: obj[k]
        };
    });
};
