using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
	public bool onGround = true;

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;

	[SerializeField]
	private float jumpPower = 1000f;


    //Component caching
    private PlayerMotor motor;
    private Animator animator;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //Calculate movement velocity as a 3D vector
        float _xMov = Input.GetAxis("Horizontal");
        float _zMov = Input.GetAxis("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        // Final movement vector
        Vector3 _velocity = (_movHorizontal + _movVertical) * speed;

        //animate movement
        animator.SetFloat("ForwardVelocity", _zMov);

        //Apply movement
		if (onGround) {
			motor.Move (_velocity);
		} else {
			motor.Move (_velocity/1.4f);
		}

        //Calculate rotation as 3D vector (turning around)
        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

        //Apply rotation
        motor.Rotate(_rotation);

        //Calculate camera rotation as 3D vector (turning around)
        float _xRot = Input.GetAxisRaw("Mouse Y");

		float _cameraRotationX = _xRot * lookSensitivity;

        //Apply camera rotation
        motor.RotateCamera(_cameraRotationX);

		RaycastHit hit;
		Vector3 physicsCentre = this.transform.position + this.GetComponent<BoxCollider>().center;
		if (Physics.Raycast(physicsCentre, Vector3.down, out hit, 1.02f)) {
			if (hit.transform.gameObject.tag != "Player") {
				onGround = true;
			}
		} else {
			onGround = false;
		}
		if(Input.GetKeyDown(KeyCode.Space)) {
			jump();
		}
    }

	//Jumping functio
	public void jump() {
		if (onGround) {
			this.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPower);
		}
	}
}