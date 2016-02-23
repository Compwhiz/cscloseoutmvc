declare module server {
	interface CloseOutOption {
		iD: number;
		parentID: number;
		code: string;
		description: string;
		color: any;
		colorHex: string;
		children: server.CloseOutOption[];
	}
	interface ColorChanger {
	}
}
