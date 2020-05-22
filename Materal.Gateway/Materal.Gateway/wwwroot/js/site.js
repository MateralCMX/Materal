function getParams() {
	const result = {};
	const params = window.location.search.substr(1).split("&");
	for (let i = 0; i < params.length; i++) {
		const temp = params[i].split("=");
		if (temp.length === 2) {
			result[temp[0]] = temp[1];
		}
	}
	return result;
}