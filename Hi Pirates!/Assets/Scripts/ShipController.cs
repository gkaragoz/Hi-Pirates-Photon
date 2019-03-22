using Photon.Pun;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ShipMotor))]
public class ShipController : MonoBehaviour
{

    private ShipMotor _shipMotor;
    private PhotonView _photonView;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();

        _shipMotor = GetComponent<ShipMotor>();
    }
    private void Start()
    {
        //photonView.Owner.GetPlayerNumber();
    }
    public void Update()
    {
        if (!_photonView.IsMine)
        {
            return;
        }

        //rotation = Input.GetAxis("Horizontal");
        //acceleration = Input.GetAxis("Vertical");

        //if (Input.GetButton("Jump") && shootingTimer <= 0.0)
        //{
        //    shootingTimer = 0.2f;

        //    photonView.RPC("Fire", RpcTarget.AllViaServer, rigidbody.position, rigidbody.rotation);
        //}

        //if (shootingTimer > 0.0f)
        //{
        //    shootingTimer -= Time.deltaTime;
        //}
    }
    public void FixedUpdate()
    {
        if (!_photonView.IsMine)
        {
            return;
        }

        //Quaternion rot = rigidbody.rotation * Quaternion.Euler(0, rotation * RotationSpeed * Time.fixedDeltaTime, 0);
        //rigidbody.MoveRotation(rot);

        //Vector3 force = (rot * Vector3.forward) * acceleration * 1000.0f * MovementSpeed * Time.fixedDeltaTime;
        //rigidbody.AddForce(force);

        //if (rigidbody.velocity.magnitude > (MaxSpeed * 1000.0f))
        //{
        //    rigidbody.velocity = rigidbody.velocity.normalized * MaxSpeed * 1000.0f;
        //}

        //CheckExitScreen();
    }
    #region COROUTINES

    //private IEnumerator WaitForRespawn()
    //{
    //    yield return new WaitForSeconds(AsteroidsGame.PLAYER_RESPAWN_TIME);

    //    _photonView.RPC("RespawnSpaceship", RpcTarget.AllViaServer);
    //}

    #endregion
    #region PUN CALLBACKS

    [PunRPC]
    public void DestroySpaceship()
    {
        if (_photonView.IsMine)
        {
            // GO TO LOBBY
            StartCoroutine("WaitForRespawn");
        }
    }

    [PunRPC]
    public void Fire(Vector3 position, Quaternion rotation, PhotonMessageInfo info)
    {
        float lag = (float)(PhotonNetwork.Time - info.SentServerTime);
        GameObject bullet;

        ///** Use this if you want to fire one bullet at a time **/
        //bullet = Instantiate(BulletPrefab, rigidbody.position, Quaternion.identity) as GameObject;
        //bullet.GetComponent<Bullet>().InitializeBullet(photonView.Owner, (rotation * Vector3.forward), Mathf.Abs(lag));


        /** Use this if you want to fire two bullets at once **/
        //Vector3 baseX = rotation * Vector3.right;
        //Vector3 baseZ = rotation * Vector3.forward;

        //Vector3 offsetLeft = -1.5f * baseX - 0.5f * baseZ;
        //Vector3 offsetRight = 1.5f * baseX - 0.5f * baseZ;

        //bullet = Instantiate(BulletPrefab, rigidbody.position + offsetLeft, Quaternion.identity) as GameObject;
        //bullet.GetComponent<Bullet>().InitializeBullet(photonView.Owner, baseZ, Mathf.Abs(lag));
        //bullet = Instantiate(BulletPrefab, rigidbody.position + offsetRight, Quaternion.identity) as GameObject;
        //bullet.GetComponent<Bullet>().InitializeBullet(photonView.Owner, baseZ, Mathf.Abs(lag));
    }

    [PunRPC]
    public void RespawnSpaceship()
    {

    }

    #endregion
    public void MoveToInput(Vector2 input)
    {
        _shipMotor.MoveToInput(input);
    }

}
