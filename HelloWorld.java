import javax.swing.JFrame;
import javax.swing.SwingUtilities;

public final class HelloWorld {
	public static void main(String[] args) {
		new Thread(){
			public void run() {
				System.out.println("Showing first frame...");
				new JFrame().show();
			}
		}.start();
		try {
			Thread.sleep(3000);
			System.out.println("Testing if just calling JFrame constructor hangs forever...");
			new JFrame();
			System.out.println("BUG IS FIXED!!! :)");
		} catch (Throwable t) {
			System.out.println(t.toString());
		}
	}
}
