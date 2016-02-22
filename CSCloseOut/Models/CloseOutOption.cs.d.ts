declare module server {
	interface CloseOutOption {
		iD: number;
		parentID: number;
		code: string;
		description: string;
		children: server.CloseOutOption[];
	}
}
