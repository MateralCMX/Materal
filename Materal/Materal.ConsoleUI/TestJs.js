/**
 * 获取面积
 * @param {Array<Array<Array<number>>>} args 
 * @example
 * [[[125, -15], [113, -22], [154, -27], [144, -15], [125, -15]]]
 */
function Handler(args) {
    let polygon = turf.polygon(args);
    return turf.area(polygon);
}